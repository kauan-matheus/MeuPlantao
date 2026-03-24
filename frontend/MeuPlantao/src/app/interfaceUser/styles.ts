import { colors } from "@/styles/colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    container: {
        flex: 1,
    },
    top: {
        width: "100%",
        height: "20%",
        backgroundColor: colors.blue[500]
    },
    content: {
        width: "100%"
    },
    navBar: {
        backgroundColor: colors.gray[600],
        borderRadius: 50,
        maxHeight: 60,
        padding: 5,
        alignSelf: "center",
        position: "absolute",
        bottom: 120
    },
    navBarContent: {
        gap: 5
    }
})