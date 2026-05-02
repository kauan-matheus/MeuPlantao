import { StyleSheet } from "react-native";
import { colors } from "@/styles/colors";

export const styles = StyleSheet.create({
    listContent: {
        width: "100%",
        marginBottom: 25
    },
    textListEmpty: {
        textAlign: "center",
        fontWeight: "bold",
        color: colors.gray[300],
        fontSize: 17,
        padding: 20
    },
    footer: {
        height: 75
    },
    modal: {
        flex: 1,
        justifyContent: "flex-end"
    },
    close: {
        padding: 10,
        alignSelf: "flex-end"
    },
    modalContent: {
        width: "100%",
        height: "90%",
        backgroundColor: colors.gray[700]
    }
})