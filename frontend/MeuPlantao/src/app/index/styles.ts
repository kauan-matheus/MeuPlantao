import { colors } from "@/styles/colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    background: {
        flex: 1
    },
    container: {
        flex: 1,
        paddingHorizontal: 40,
        alignItems: "center",
        justifyContent: "flex-end",
        bottom: 90
    },
    modalContent: {
        width: "100%",
        height: "75%",
        backgroundColor: colors.gray[700],
        borderTopRightRadius: 30,
        borderTopLeftRadius: 30,
        boxShadow: "0 4px 6px rgba(0, 0, 0, 0.2)",
        alignItems: "center",
        padding: 24,
        paddingVertical: 20,
        gap: 25
    },
    titleModal: {
        fontSize: 40,
        color: colors.blue[400],
        fontFamily: "Poppins-Bold"
    },
    logo: {
        width: 50,
        height: 50
    },
    form: {
        width: "100%",
        gap: 20,
        paddingHorizontal: 30,
        alignItems: "center"
    },
    link: {
        color: colors.blue[400],
        textDecorationLine: "underline",
        fontFamily: "Poppins-Regular",
        marginTop: 20
    },
    close: {
        left: "47%"
    },
    modal: {
        flex: 1,
        justifyContent: "flex-end"
    },
    error: {
        backgroundColor: colors.red[400],
        paddingHorizontal: 8,
        paddingVertical: 2,
        borderRadius: 10,
        gap: 5,
        flexDirection: "row"
    },
    textError: {
        fontFamily: "Poppins-Regular",
        color: colors.red[300],
    }
})