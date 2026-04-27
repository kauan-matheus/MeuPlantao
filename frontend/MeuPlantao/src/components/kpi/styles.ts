import { StyleSheet } from "react-native";
import { colors } from "@/styles/colors";

export const styles = StyleSheet.create({
    container: {
        width: 85,
        alignItems: "center",
        justifyContent: "center",
        paddingHorizontal: 5,
        gap: 5
    },
    value: {
        fontFamily: "Poppins-Bold",
        fontSize: 25,
        paddingHorizontal: 20,
        paddingVertical: 5,
        borderRadius: 18,
        color: colors.gray[700]
    },
    text: {
        fontFamily: "Poppins-Regular",
        fontSize: 12
    }
})