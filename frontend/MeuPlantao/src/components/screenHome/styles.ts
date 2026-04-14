import { colors } from "@/styles/colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20
    },
    inProgress: {
        backgroundColor: colors.gray[600],
        borderRadius: 15,
        paddingTop: 10,
        paddingHorizontal: 10
    },
    title: {
        fontFamily: "Poppins-Bold",
        fontSize: 12,
        color: colors.gray[200],
        paddingHorizontal: 10
    }
})