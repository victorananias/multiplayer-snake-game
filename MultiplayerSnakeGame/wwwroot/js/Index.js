"use strict"

const KEYS = ['w', 'a', 's', 'd', ' ']

import Connection from './Connection.js' 
import Keyboard from './Keyboard.js'

const keyboard = new Keyboard()

Connection.start()
Connection.registerUpdate()
Connection.registerGameOver()


KEYS.forEach(key => {
    keyboard.onPress(key, () => Connection.keyPressed(key))
    keyboard.onRelease(key, () => Connection.keyReleased(key))
})
