import { colors } from "@/styles/colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    background: {
        flex: 1
    },
    container: {
        flex: 1,
        padding: 40,
        alignItems: "center",
        justifyContent: "flex-end",
        bottom: 50

    },
    modalContent: {
        width: "100%",
        height: "75%",
        backgroundColor: colors.gray[600],
        borderTopRightRadius: 30,
        borderTopLeftRadius: 30,
        boxShadow: "0 4px 6px rgba(0, 0, 0, 0.2)",
        alignItems: "center",
        padding: 24,
        paddingVertical: 20,
        gap: 50
    },
    titleModal: {
        fontSize: 40,
        // textShadowColor: colors.gray[100],
        // textShadowOffset: { width: 1, height: 1 },
        // textShadowRadius: 5,
        color: colors.gray[100],
        fontWeight: "bold"
    },
    logo: {
        width: 50,
        height: 50
    },
    form: {
        width: "100%",
        gap: 20,
        paddingHorizontal: 30,
        alignItems: "center"
    },
    viewPass: {
        position: "absolute"
    },
    link: {
        color: colors.blue[300],
        textDecorationLine: "underline",
    },
    close: {
        left: "47%"
    },
    modal: {
        flex: 1,
        justifyContent: "flex-end"
    }
})