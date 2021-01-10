import Connection from "./Connection.js"
import { $, $$ } from "./helpers.js"

export default {
    scoreList: [],
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
        [...$$('#score tbody tr')].forEach(e => e.remove())
    },
    draw() {
        this.scoreList.forEach(score => {
            $('#score').innerHTML += `
                <tr class="${score.snakeId === Connection.getPlayerId() ? 'score-current-player ' : ''}">
                    <td>${score.snakeId}</td>
                    <td>${score.points}</td>
                </tr>
            `
        })
    }
}