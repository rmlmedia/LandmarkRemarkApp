import React, { useState, useEffect } from 'react'
import { StyleSheet, Text, View, Image, TouchableOpacity, TextInput } from 'react-native'
import MapView, { Marker, Region } from 'react-native-maps'
import { getLandmarks, getUsers, addLandmark } from './services/api'
import { Landmark } from './models/Landmark'
import { User } from './models/User'
import * as Location from 'expo-location'

export default function App() {
  // Set a default location for the map
  const defaultRegion = {
    latitude: -37.807,
    longitude: 144.991,
    latitudeDelta: 0.1,
    longitudeDelta: 0.1,
  } as Region

  // Initialse the state variables
  const [landmarks, setLandmarks] = useState([] as Landmark[])
  const [users, setUsers] = useState([] as User[])
  const [currentUserIndex, setCurrentUserIndex] = useState(0)
  const [currentLocation, setCurrentLocation] = useState(defaultRegion)
  const [comment, setComment] = useState('')
  const [searchText, setSearchText] = useState('')

  // Retrieve the Landmarks and Users from the database on load
  useEffect(() => {
    var fetchData = async () => {
      const { status } = await Location.requestPermissionsAsync();
      if (status === 'granted') {
        const location = await Location.getCurrentPositionAsync({});
        setCurrentLocation({
          latitude: location.coords.latitude,
          longitude: location.coords.longitude,
          latitudeDelta: currentLocation.latitudeDelta,
          longitudeDelta: currentLocation.longitudeDelta,
        } as Region)
      }

      setLandmarks(await getLandmarks())
      setUsers(await getUsers())
    }

    if (landmarks.length === 0) {
      fetchData()
    }
  }, []);

  // Returns if one string contains another
  var containsText = (srcStr: string, text: string): boolean => {
    return text !== '' && srcStr.toLowerCase().includes(text.toLowerCase())
  }

  // Search landmarks
  var matchesSearchText = (landmarkIndex: number) => {
    var landmark = landmarks[landmarkIndex]
    var fullname = users.find(u => u.username === landmark.username)?.fullname ?? ''
    return containsText(landmark.comment, searchText) || containsText(landmark.username, searchText) || containsText(fullname, searchText)
  }

  // Run the search
  var runSearch = () => {
    // Pin colour based on search text is set on render so we need to force a redraw by reading the pins
    var dirtiedLandmarks = [] as Landmark[]
    setLandmarks(dirtiedLandmarks)
    setTimeout(() => {
      dirtiedLandmarks = dirtiedLandmarks.concat(landmarks)
      console.log(dirtiedLandmarks)
      setLandmarks(dirtiedLandmarks)
    }, 50)
  }

  // Changes the user
  var changeUser = () => {
    setCurrentUserIndex((currentUserIndex + 1) % users.length)
  }

  // Adds a new landmark to the map and the database
  var addNewLandmark = async () => {
    // Build a new landmark object
    var landmark = {} as Landmark
    landmark.username = users[currentUserIndex].username
    landmark.latitude = currentLocation.latitude
    landmark.longitude = currentLocation.longitude
    landmark.comment = comment

    // Post the landmark to the server
    var newLandmark = await addLandmark(landmark)
    console.log(newLandmark)

    // Add the landmark to the UI
    setLandmarks(landmarks.concat(newLandmark))
  }

  return (
    <>
      <View style={styles.container}>
        <MapView style={styles.map} region={currentLocation} onRegionChangeComplete={region => setCurrentLocation(region)}>
          {landmarks.map((landmark, index) =>
            <Marker
              key={landmark.id}
              coordinate={{ latitude: landmark.latitude, longitude: landmark.longitude }}
              title={(users.find(u => u.username === landmark.username))?.fullname}
              description={landmark.comment}
              pinColor={matchesSearchText(index) ? '#3366ff' : '#ff0000'}
            />
          )}
        </MapView>
        <Image source={require('./images/crosshair.png')} style={styles.crosshair} />
        <View style={styles.toolbar}>
          <View style={styles.toolbarLabel}>
            <Text style={styles.label}>Add note for {users[currentUserIndex]?.fullname ?? '...'}</Text>
            <TouchableOpacity onPress={() => changeUser()}>
              <Text style={styles.link}> [Change]</Text>
            </TouchableOpacity>
          </View>
          <View style={styles.toolbarInput}>
            <TextInput
              style={styles.textbox}
              multiline
              numberOfLines={1}
              maxLength={500}
              placeholder='Comment'
              onChangeText={(text) => setComment(text)}
            />
            <TouchableOpacity onPress={() => addNewLandmark()}>
              <Text style={styles.button}>Add</Text>
            </TouchableOpacity>
          </View>
          <View style={styles.toolbarLabel}>
            <Text style={styles.label}>Highlight Landmarks</Text>
          </View>
          <View style={styles.toolbarInput}>
            <TextInput
              style={styles.textbox}
              placeholder='Search by comment or username'
              onChangeText={(text) => setSearchText(text)}
            />
            <TouchableOpacity onPress={() => runSearch()}>
              <Text style={styles.button}>Search</Text>
            </TouchableOpacity>
          </View>
        </View>
      </View>
    </>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
  map: {
    flex: 1,
    width: '100%',
  },
  crosshair: {
    position: 'absolute',
    top: '50%',
    left: '50%',
    width: 50,
    height: 50,
    marginLeft: -25,
    marginTop: -125,
  },
  toolbar: {
    flex: 0,
    justifyContent: 'center',
    paddingHorizontal: 20,
    height: 200,
    width: '100%',
  },
  toolbarLabel: {
    flexDirection: 'row',
    paddingBottom: 10,
    justifyContent: 'flex-start',
  },
  toolbarInput: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingBottom: 5,
  },
  label: {
    fontSize: 16,
    fontWeight: 'bold',
  },
  link: {
    fontSize: 16,
    color: '#3366ff',
  },
  textbox: {
    borderRadius: 5,
    borderColor: '#dddddd',
    borderWidth: 1,
    padding: 10,
    flex: 1,
  },
  button: {
    borderRadius: 5,
    backgroundColor: '#3366ff',
    textAlign: 'center',
    fontSize: 13,
    color: '#ffffff',
    padding: 16,
    marginLeft: 10,
    width: 80,
  }
})
