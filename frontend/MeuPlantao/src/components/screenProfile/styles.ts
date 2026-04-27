import { colors } from "@/styles/colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        alignItems: "center",
        flexDirection: "column",
        justifyContent: "center"
    },
    imageProfile: {
        width: 210,
        height: 210,
        borderRadius: 200,
        marginBottom: 20
    },
    dataUser: {
        width: "70%",
        gap: 5,
        paddingBottom: 50,
        paddingTop: 30
    }
})