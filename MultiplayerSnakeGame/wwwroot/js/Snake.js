export default class Snake {

    constructor(snake, context, color) {
        this.alive = true
        Object.assign(this, snake)

        this.color = color
        
        this.context = context
    }

    draw() {
        this.context.fillStyle = this.color
        this.context.fillRect(this.head.x, this.head.y, this.head.size, this.head.size)

        for (let i = 0; i < this.body.length; i++) {
            const piece = this.body[i]
            this.context.fillStyle = this.color
            this.context.fillRect(piece.x, piece.y, piece.size, piece.size)
        }
    }
}