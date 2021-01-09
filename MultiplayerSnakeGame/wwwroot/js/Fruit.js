export default class Fruit {
    constructor(fruit, context) {
        Object.assign(this, fruit)
        this.context = context
    }

    draw() {
        this.context.fillStyle = '#c54c4c'
        this.context.fillRect(this.x, this.y, this.size, this.size)
    }
}