import Connection from "./Connection.js"
import KeyboardEvents from "./KeyboardEvents.js"

export default {
    keys: [
        { key: 'ArrowUp', action: 'Up' },
        { key: 'ArrowLeft', action: 'Left' },
        { key: 'ArrowDown', action: 'Down' },
        { key: 'ArrowRight', action: 'Right' },
        { key: 'w', action: 'Up' },
        { key: 'a', action: 'Left' },
        { key: 's', action: 'Down' },
        { key: 'd', action: 'Right' },
    ],
    enable() {
        this.keys.forEach(key => {
            KeyboardEvents.onPress(key.key, () => Connection.startAction(key.action))
            KeyboardEvents.onRelease(key.key, () => Connection.stopAction(key.action))
        })
    }
}