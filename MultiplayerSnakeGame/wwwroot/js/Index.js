"use strict"

import Connection from './Connection.js'
import Control from './Control.js'
import Keyboard from './Keyboard.js'
import View from './View.js'
import Score from './Score.js'

Connection.establishConnectionAndJoinGame()
    .then(() => {
        Connection.on("Update", onUpdate)
        Connection.on("Win", onWin)
        Connection.on("Lose", onLose)
        Control.registerKeys(new Keyboard);
    })

function onUpdate({ snakes, fruits, scoreList }) {
    View.clear()
    View.drawSnakes(snakes)
    View.drawFruits(fruits)
    Score.update(scoreList)
}

function onWin() {
    document.querySelector('.game-over').classList.add('display-block', 'animation-running')
    const gameOverTextElement = document.querySelector('.game-over-text')
    gameOverTextElement.classList.add('win')
    gameOverTextElement.textContent = 'You Win'
}

function onLose() {
    document.querySelector('.game-over').classList.add('display-block', 'animation-running')
    const gameOverTextElement = document.querySelector('.game-over-text')
    gameOverTextElement.classList.add('lose')
    gameOverTextElement.textContent = 'You Lose'
}



