import { StyleSheet } from "react-native"

export const styles = StyleSheet.create({
    container: {
        flex: 1,
        alignItems: "center",
        paddingHorizontal: 20,
        gap: 10
    },
    map: {
        width: "100%",
        height: 200
    },
    title: {
        fontFamily: "Poppins-Bold",
        fontSize: 18
    },
    data: {
        width: "100%",
        gap: 30
    },
    row: {
        width: "100%",
        flexDirection: "row",
        justifyContent: "space-between",
        paddingHorizontal: 25
    },
    col: {
        width: "auto"
    }
})