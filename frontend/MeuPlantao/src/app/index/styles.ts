import { colors } from "@/styles/colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 40,
        justifyContent: "center",
    },
    modalLogin: {
        width: "100%",
        backgroundColor: colors.gray[600],
        borderRadius: 30,
        boxShadow: "0 4px 6px rgba(0, 0, 0, 0.2)",
        alignItems: "center",
        justifyContent: "center",
        padding: 24,
        paddingVertical: 50,
        gap: 15
    },
    title: {
        fontSize: 40,
        textShadowColor: colors.gray[100],
        textShadowOffset: { width: 1, height: 1 },
        textShadowRadius: 5,
        color: colors.gray[100]
    },
    logo: {
        width: 50,
        height: 50
    },
    form: {
        width: "100%",
        gap: 20,
        alignItems: "center"
    },
    viewPass: {
        position: "absolute"
    },
    link: {
        color: colors.blue[300],
        textDecorationLine: "underline"
    }
})