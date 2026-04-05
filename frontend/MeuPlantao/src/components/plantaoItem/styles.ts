import { StyleSheet } from "react-native";
import { colors } from "@/styles/colors";

export const styles = StyleSheet.create({
    container: {
        flex: 1,
        borderBottomColor: colors.gray[400],
        borderBottomWidth: 1,
        padding: 15,
        gap: 6
    },
    title: {
        fontWeight: "bold",
        fontSize: 15,
        color: colors.blue[400]
    },
    subTitle: {
        fontSize: 12
    },
    info: {
        flexDirection: "row",
        justifyContent: "space-between"
    },
    textInfo: {
        fontSize: 13
    }
})