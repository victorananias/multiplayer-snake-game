import Connection from "./Connection.js"
import Keyboard from "./Keyboard.js"

export default {
    keys: ['w', 'a', 's', 'd', ' '],
    registerKeys() {
        ['w', 'a', 's', 'd', ' '].forEach(key => {
            Keyboard.onPress(key, () => Connection.invokeKeyPressed(key))
            Keyboard.onRelease(key, () => Connection.invokeKeyReleased(key))
        })
    }
}