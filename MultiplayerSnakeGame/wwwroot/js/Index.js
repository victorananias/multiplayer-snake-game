"use strict"

const deadColor = 'grey'
const playerColor = '#4bb84b'
const enemyColor = 'yellow'

const KEYS = ['w', 'a', 's', 'd', ' ']
const canvas = document.querySelector('#game')
const context = canvas.getContext('2d')

import Background from './Background.js'
import Keyboard from './Keyboard.js'
import Snake from './Snake.js'
import Fruit from './Fruit.js'

const background = new Background(context)
const keyboard = new Keyboard()

const connection = new signalR.HubConnectionBuilder().withUrl("gamehub").build()

let scoreList = [];

connection.start()
    .then(joinGame)
    .catch(err => console.error(err.toString()))

connection.on("Update", (data) => {
    context.clearRect(0, 0, 500, 500)
    background.draw()

    data.snakes.forEach(s => {
        const snake = new Snake(s, context, snakeColor(s, connection.connectionId))
        snake.draw()
    })

    data.fruits.forEach(f => {
        const fruit = new Fruit(f, context)
        fruit.draw()
    })

    if (JSON.stringify(scoreList) !== JSON.stringify(data.scoreList)) {
        scoreList = data.scoreList
        updateScore()
    }
})

connection.on("Win", () => {
    document.querySelector('.game-over').classList.add('display-block', 'animation-running')
    const gameOverTextElement = document.querySelector('.game-over-text')
    gameOverTextElement.classList.add('win')
    gameOverTextElement.textContent = 'You Win'
})

connection.on("Lose", () => {
    document.querySelector('.game-over').classList.add('display-block', 'animation-running')
    const gameOverTextElement = document.querySelector('.game-over-text')
    gameOverTextElement.classList.add('lose')
    gameOverTextElement.textContent = 'You Lose'
})

KEYS.forEach(key => {
    keyboard.onPress(key, () => keyPressed(key))
    keyboard.onRelease(key, () => keyReleased(key))
})

function keyPressed(key) {
    connection.invoke("KeyPressed", key).catch(err => console.error(err.toString()))
}

function keyReleased(key) {
    connection.invoke("KeyReleased", key).catch(err => console.error(err.toString()))
}

function joinGame() {
    connection.invoke("JoinGame", gameId)
        .catch(err => console.error(err.toString()))
}

function updateScore() {
    [...document.querySelectorAll('#score tbody tr')].forEach(e => e.remove())

    scoreList.forEach(s => {
        document.querySelector('#score').innerHTML += `
      <tr class="${s.snakeId === connection.connectionId ? 'score-current-player ' : ''}">
        <td>${s.snakeId}</td>
        <td>${s.points}</td>
      </tr>
    `
    })
}

function snakeColor(snake, playerId) {
    if (!snake.alive) {
        return deadColor
    } else if (!playerId) {
        return playerColor
    } else {
        return snake.id == playerId ? playerColor : enemyColor
    }
}