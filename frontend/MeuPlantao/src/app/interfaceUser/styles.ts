import { colors } from "@/styles/colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    container: {
        flex: 1
    },
    top: {
        width: "100%",
        height: "17%",
        backgroundColor: colors.blue[500],
        paddingHorizontal: 25,
        paddingTop: 30
    },
    topDiv: {
        flex: 1,
        justifyContent: "center"
    },
    topText: {
        fontSize: 17,
        fontWeight: "bold",
        color: colors.gray[700]
    },
    content: {
        flex: 1
    },
    navBar: {
        backgroundColor: colors.gray[600],
        borderRadius: 50,
        maxHeight: 60,
        padding: 5,
        alignSelf: "center",
        position: "absolute",
        bottom: 60,
        boxShadow: "0 1px 5px 1px rgba(0, 0, 0, 0.1)"
    },
    navBarContent: {
        gap: 5
    },
    imageProfile: {
        width: 50,
        height: 50,
        borderRadius: 50
    }
})