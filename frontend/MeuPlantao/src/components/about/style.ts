import { colors } from "@/styles/colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    container: {
        padding: 2
    },
    title: {
        fontFamily: "Poppins-Bold",
        color: colors.blue[400],
        fontSize: 13
    },
    text: {
        fontFamily: "Poppins-Regular",
        borderBottomWidth: 1,
        borderColor: colors.gray[500],
        paddingBottom: 2,
        color: colors.gray[100],
        fontSize: 15
    }
})