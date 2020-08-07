import { Landmark } from '../models/Landmark';
import { User } from '../models/User';

const apiBaseUri = 'http://192.168.0.150:5000/api/'
const getLandmarksUri = apiBaseUri + 'landmarks'
const addLandmarkUri = apiBaseUri + 'landmarks/add'
const getUsersUri = apiBaseUri + 'users'

// Gets the users from the API
export async function getUsers(): Promise<User[]> {
  var users: User[] = []

  console.log('Retrieving Users from ' + getUsersUri)

  try {
    var response = await fetch(getUsersUri)
    users = await response.json()
    console.log('Users retrieved')
  }
  catch(error) {
    console.log('Error: ' + error)
  }
  
  return users
}

// Get the saved landmarks from the API
export async function getLandmarks(): Promise<Landmark[]> {
  var landmarks: Landmark[] = []

  console.log('Retrieving Landmarks from ' + getLandmarksUri)

  try {
    var response = await fetch(getLandmarksUri)
    landmarks = await response.json()
    console.log('Landmarks retrieved')
  }
  catch(error) {
    console.log('Error: ' + error)
  }

  return landmarks
}

// Adds a new landmark via the API
export async function addLandmark(landmark: Landmark): Promise<Landmark> {
  var landmarkResponse = {} as Landmark

  console.log('Sending Landmark to be stored in database via ' + addLandmarkUri)

  try {
    var response = await fetch(addLandmarkUri, {
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      method: 'POST',
      body: JSON.stringify(landmark),
    })
    landmarkResponse = await response.json()
    console.log('Landmark saved')
  }
  catch(error) {
    console.log('Error: ' + error)
  }

  return landmarkResponse
}