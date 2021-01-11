import Connection from "./Connection.js"
import KeyboardEvents from "./KeyboardEvents.js"

export default {
    keys: ['w', 'a', 's', 'd', ' '],
    enable() {
        this.keys.forEach(key => {
            KeyboardEvents.onPress(key, () => Connection.invokeKeyPressed(key))
            KeyboardEvents.onRelease(key, () => Connection.invokeKeyReleased(key))
        })
    }
}