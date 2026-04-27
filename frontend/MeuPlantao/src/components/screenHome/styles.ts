import { colors } from "@/styles/colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        gap: 10
    },
    card: {
        backgroundColor: colors.gray[600],
        borderRadius: 15,
        paddingTop: 10,
        paddingHorizontal: 10,
        gap: 5
    },
    title: {
        fontFamily: "Poppins-Bold",
        fontSize: 12,
        color: colors.blue[400],
        paddingHorizontal: 10
    },
    list: {
        flex: 1,
        backgroundColor: colors.gray[600],
        borderRadius: 20,
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