import { colors } from "@/styles/colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    container: {
        flex: 1,
        paddingHorizontal: 24,
        paddingTop: 40,
        backgroundColor: colors.white[100],
        alignItems: "center",
    },

    imageProfile: {
        width: 150,
        height: 150,
        borderRadius: 75,

        borderWidth: 4,
        borderColor: colors.blue[400],

        marginBottom: 16,
    },

    dataUser: {
        width: "100%",
        backgroundColor: colors.gray[600],
        borderRadius: 15,
        paddingVertical: 10,
        paddingHorizontal: 10,
        gap: 10
    },

    name: {
        fontFamily: "Poppins-Bold",
        fontSize: 24,
        color: colors.blue[400],
        paddingHorizontal: 10
    },

    role: {
        fontSize: 16,
        fontFamily: "Poppins-Bold",
        color: colors.blue[400],
        paddingHorizontal: 10
    },

    imageContainer: {
        position: "relative",
    },

    editBadge: {
        position: "absolute",

        right: 10,
        bottom: 10,

        width: 40,
        height: 40,

        borderRadius: 20,

        backgroundColor: colors.blue[400],

        justifyContent: "center",
        alignItems: "center",
    },
});