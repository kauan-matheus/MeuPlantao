import { StyleSheet } from "react-native";
import { colors } from "@/styles/colors";

export const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        gap: 5
    },
    list: {
        flex: 1,
        backgroundColor: colors.gray[600],
        borderRadius: 25,
        alignItems: "center",
        paddingHorizontal: 15,
        paddingTop: 10
    },
    kpis: {
        paddingHorizontal: 10,
        paddingVertical: 5,
        gap: 5,
        flexDirection: "row",
        justifyContent: "center"
    }
})