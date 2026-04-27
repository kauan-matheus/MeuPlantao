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
        fontFamily: "Poppins-Bold",
        fontSize: 15,
        color: colors.blue[400]
    },
    subTitle: {
        fontFamily: "Poppins-Regular",
        fontSize: 11
    },
    info: {
        flexDirection: "row",
        justifyContent: "space-between"
    },
    textInfo: {
        fontSize: 12,
        fontFamily: "Poppins-Regular"
    },
    nav: {
        flexDirection: "row",
        alignItems: "center",
        justifyContent: "space-between"
    },
    status: {
        fontSize: 9,
        fontFamily: "Poppins-Bold",
        paddingVertical: 2,
        paddingHorizontal: 6,
        borderRadius: 10,
        color: colors.gray[700]
    }
})