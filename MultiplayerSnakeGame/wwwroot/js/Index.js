"use strict"

import Connection from './Connection.js'
import Control from './Control.js'
import Keyboard from './Keyboard.js'
import View from './View.js'

Keyboard.enable()

Connection.establishConnectionAndJoinGame()
    .then(() => {
        Connection.on("Update", View.onUpdate.bind(View))
        Connection.on("Win", View.onGameOver.bind(View, 'win'))
        Connection.on("Lose", View.onGameOver.bind(View, 'lose'))
        Control.registerKeys()
    })