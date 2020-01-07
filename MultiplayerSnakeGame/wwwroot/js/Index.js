"use strict"

const KEYS = ['w', 'a', 's', 'd', ' ']
const canvas = document.querySelector('#game')
const context = canvas.getContext('2d')

const background = new Background(context)
const keyboard = new Keyboard()

const connection = new signalR.HubConnectionBuilder().withUrl("gamehub").build()

let scores = [];

connection.start()
    .then(() => {
        console.log('connected')
        joinGame()
    })
    .catch(err => console.error(err.toString()))

connection.on("Update", ({fruits, snakes, scoreList }) => {
    context.clearRect(0, 0, 500, 500)

    background.draw()

    snakes.forEach(s => {
        const snake = new Snake(s, context, snakeColor(s, connection.connectionId))
        snake.draw()
    })

    fruits.forEach(f => {
        const fruit = new Fruit(f, context)
        fruit.draw()
    })

    if (JSON.stringify(scores) !== JSON.stringify(scoreList)) {
        scores = scoreList
        updateScore()
    }
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
        .then(() => console.log("joined game " + gameId))
        .catch(err => console.error(err.toString()))
}

function updateScore() {
    [...document.querySelectorAll('#score tbody tr')].forEach(e => e.remove())

    scores.forEach(s => {
        document.querySelector('#score').innerHTML += `
            <tr class="${s.snakeId === connection.connectionId ? 'score-current-player ' : ''}">
                <td>${s.snakeId}</td>
                <td>${s.points}</td>
            </tr>
        `
    })
}

function snakeColor(snake, playerId) {
    const deadColor = 'grey'
    const playerColor = '#4bb84b'
    const enemyColor = 'yellow'

    if (!snake.alive) {
        return deadColor
    } else if (!playerId) {
        return playerColor
    } else {
        return snake.id === playerId ? playerColor : enemyColor
    }
}