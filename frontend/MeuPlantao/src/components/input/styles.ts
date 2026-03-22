import { colors } from "@/styles/colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    container: {
        width: "100%",
        height: 50,
        backgroundColor: colors.gray[500],
        borderRadius: 25,
        paddingHorizontal: 10,
        boxShadow: "0 4px 6px rgba(0, 0, 0, 0.3)",
        flexDirection: "row",
        justifyContent: "space-between",
        alignItems: "center"
    },
    group: {
        width: "85%",
        flexDirection: "row",
        alignItems: "center"
    },
    input: {
        fontSize: 17,
        fontWeight: "bold",
        width: "100%"
    }
})