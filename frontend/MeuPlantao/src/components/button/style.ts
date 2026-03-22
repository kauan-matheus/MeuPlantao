import { colors } from "@/styles/colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    container: {
        height: 50,
        width: "50%",
        backgroundColor: colors.blue[200],
        borderRadius: 30,
        justifyContent: "center",
        alignItems: "center",
        marginTop: 20,
        boxShadow: "0 4px 6px rgba(0, 0, 0, 0.2)"
    },
    title: {
        color: colors.gray[600],
        fontSize: 18,
        fontWeight: "600"
    }
})