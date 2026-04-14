import { StyleSheet } from "react-native";
import { colors } from "@/styles/colors";

export const styles = StyleSheet.create({
    container: {
        flex: 1,
        paddingHorizontal: 20,
        paddingTop: 10,
        paddingBottom: 20,
        gap: 10
    },
    calendar: {
        borderRadius: 25,
        paddingBottom: 10
    },
    list: {
        flex: 1,
        backgroundColor: colors.gray[600],
        borderRadius: 25,
        alignItems: "center",
        paddingHorizontal: 15,
        paddingTop: 10
    }
})