import { StyleSheet } from "react-native";
import { colors } from "@/styles/colors";

export const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        gap: 5
    },
    card: {
        backgroundColor: colors.gray[600],
        borderRadius: 15,
        paddingTop: 10,
        paddingHorizontal: 10,
        gap: 5
    },
    title: {
        fontFamily: "Poppins-Bold",
        fontSize: 12,
        color: colors.gray[200],
        paddingHorizontal: 10
    },
    row: {
        flexDirection: "row",
        width: "100%",
        justifyContent: "space-around",
        gap: 5
    },
    col: {
        flexDirection: "column",
        width: "50%",
        backgroundColor: colors.gray[800],
        paddingVertical: 10,
        paddingHorizontal: 20,
        borderRadius: 10,
        alignItems: "center"
    },
    textValue: {
        fontFamily: "Poppins-Regular",
        fontSize: 17
    },
})