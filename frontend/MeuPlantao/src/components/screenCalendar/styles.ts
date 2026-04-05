import { StyleSheet } from "react-native";
import { colors } from "@/styles/colors";

export const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        gap: 10
    },
    calendar: {
        borderRadius: 25,
        padding: 10
    },
    list: {
        flex: 1,
        backgroundColor: colors.gray[600],
        borderRadius: 25,
        alignItems: "center",
        paddingHorizontal: 15,
        gap: 10
    },
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
    }
})