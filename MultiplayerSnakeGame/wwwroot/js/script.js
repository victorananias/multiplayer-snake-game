"use strict"

const $ = document.querySelector.bind(document);

const MOVE_LEFT = 'a',
  MOVE_RIGHT = 'd',
  MOVE_UP = 'w',
  MOVE_DOWN = 's',
  PAUSE = ' ';

const canvas = $('#game')
const context = canvas.getContext('2d')

const background = new Background(context)
const keyboard = new Keyboard()

const connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:5001/gamehub").build()

let scoreList = [];

connection.start()
  .then(() => {
    console.log('connected')
    joinGame()
  })
  .catch(err => console.error(err.toString()))

connection.on("Update", (data) => {
  context.clearRect(0, 0, 500, 500)

  background.draw()

  data.snakes.forEach(s => {
    const snake = new Snake(s, context)

    if (snake.id != connection.connectionId) {
      snake.color = 'yellow';
    }
    
    snake.draw()
  })

  data.fruits.forEach(f => {
    const fruit = new Fruit(f, context)
    fruit.draw()
  })

  if (JSON.stringify(scoreList) != JSON.stringify(data.scoreList)) {
    scoreList = data.scoreList
    updateScore()
  }
})

keyboard.onPress(MOVE_LEFT, () => {
  sendMove('left')
})
keyboard.onPress(MOVE_RIGHT, () => {
  sendMove('right')
})
keyboard.onPress(MOVE_UP, () => {
  sendMove('up')
})
keyboard.onPress(MOVE_DOWN, () => {
  sendMove('down')
})

keyboard.onPress(PAUSE, () => {
  sendMove('')
})


keyboard.onRelease(MOVE_LEFT, () => {
  reduceSpeed()
})
keyboard.onRelease(MOVE_RIGHT, () => {
  reduceSpeed()
})
keyboard.onRelease(MOVE_UP, () => {
  reduceSpeed()
})
keyboard.onRelease(MOVE_DOWN, () => {
  reduceSpeed()
})


function updateScore() {
  [...document.querySelectorAll('#score tbody tr')].forEach(e => e.remove())

  scoreList.forEach(s => {
      $('#score').innerHTML += `
      <tr class="${s.playerId == connection.connectionId ? 'score-current-player ' : ''}">
        <td>${s.playerId}</td>
        <td>${s.points}</td>
      </tr>
    `
  })
}

function joinGame() {
  connection.invoke("JoinGame", gameId).catch(err => console.error(err.toString()))
}

function sendMove(pos) {
  connection.invoke("Move", pos).catch(err => console.error(err.toString()))
}

function reduceSpeed() {
  connection.invoke("ReduceSnakeSpeed").catch(err => console.error(err.toString()))
}