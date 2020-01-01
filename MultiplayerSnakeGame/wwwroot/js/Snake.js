class Snake {

  constructor(snake, context) {
    this.alive = true
    Object.assign(this, snake)
    this.headColor = '#4bb84b'
    this.bodyColor = '#4bb84b'
    this.deadColor = 'grey'
    this.context = context
  }

  draw() {
    if (!this.alive) {
      this.headColor = this.deadColor
      this.bodyColor = this.deadColor
    }

    this.context.fillStyle = this.headColor
    this.context.fillRect(this.head.x, this.head.y, this.head.size, this.head.size)
    
    for (let i = 0; i < this.body.length; i++) {
      const piece = this.body[i]
      this.context.fillStyle = this.bodyColor
      this.context.fillRect(piece.x, piece.y, piece.size, piece.size)
    }
  }
}