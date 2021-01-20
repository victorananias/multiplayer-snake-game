"use strict"

import Connection from './Connection.js'
import Control from './Control.js'
import KeyboardEvents from './KeyboardEvents.js'
import View from './View.js'

Connection.establishConnectionAndJoinGame()
    .then(() => {
        Connection.on("Update", (data) => View.onUpdate(data))
        Connection.on("Win", () => View.onWin())
        Connection.on("Lose", () => View.onLose())
        KeyboardEvents.enable()
        Control.enable()
    })