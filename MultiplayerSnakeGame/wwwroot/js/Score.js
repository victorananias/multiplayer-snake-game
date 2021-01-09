import Connection from "./Connection.js"

export default {
    scoreList: [],
    scoreElement: document.querySelector('#score'),
    update(scoreList) {
        if (this.isEqualToCurrentScore(scoreList)) {
            return
        }


        this.scoreList = scoreList
        this.clean()
        this.draw()
    },
    isEqualToCurrentScore(scoreList) {
        return JSON.stringify(this.scoreList) == JSON.stringify(scoreList)
    },
    clean() {
        [...document.querySelectorAll('#score tbody tr')].forEach(e => e.remove())
    },
    draw() {
        this.scoreList.forEach(score => {
            this.scoreElement.innerHTML += `
                <tr class="${score.snakeId === Connection.getPlayerId() ? 'score-current-player ' : ''}">
                    <td>${score.snakeId}</td>
                    <td>${score.points}</td>
                </tr>
            `
        })
    }
}