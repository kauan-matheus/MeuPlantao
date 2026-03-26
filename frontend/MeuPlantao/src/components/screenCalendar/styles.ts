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
        padding: 15,
        gap: 10
    },
    listTitle: {
        fontSize: 16,
        fontWeight: "bold",
        color: colors.gray[200]
    }
})