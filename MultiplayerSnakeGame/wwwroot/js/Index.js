"use strict"

import Connection from './Connection.js'
import Control from './Control.js'
import Keyboard from './Keyboard.js'

Connection.start()
Connection.registerUpdate()
Connection.registerGameOver()
Control.registerKeys(new Keyboard);



