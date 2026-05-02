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
    modalContentLogin: {
        flex: 1,
        width: "100%",
        minHeight: "50%",
        backgroundColor: colors.gray[700],
        borderTopRightRadius: 30,
        borderTopLeftRadius: 30,
        boxShadow: "0 4px 6px rgba(0, 0, 0, 0.2)",
        alignItems: "center",
        paddingTop: 65,
        paddingVertical: 20,
        gap: 10
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
        marginTop: 40
    },
    close: {
        padding: 10,
        alignSelf: "flex-end"
    },
    modalLogin: {
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
    },
    modalRegister: {
        flex: 1
    },
    modalContentRegister: {
        flex: 1,
        width: "100%",
        backgroundColor: colors.gray[600],
        alignItems: "center",
        gap: 20,
        paddingTop: 5
    },
    formGroup: {
        width: "100%",
        gap: 10
    },
    optionsDocument: {
        width: "100%",
        flexDirection: "row",
        justifyContent: "space-evenly"
    },
    optionText: {
        fontFamily: "Poppins-Bold",
        fontSize: 13,
        color: colors.gray[200]
    },
    option: {
        borderColor: colors.blue[500],
        width: "25%",
        alignItems: "center"
    }
})