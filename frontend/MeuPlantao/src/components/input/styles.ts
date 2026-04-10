import { colors } from "@/styles/colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    container: {
        width: "100%",
        height: 50,
        backgroundColor: colors.gray[800],
        borderRadius: 25,
        paddingHorizontal: 16,
        flexDirection: "row",
        justifyContent: "space-between",
        alignItems: "center"
    },
    input: {
        fontSize: 15,
        fontFamily: "Poppins-Regular",
        width: "90%"
    }
})