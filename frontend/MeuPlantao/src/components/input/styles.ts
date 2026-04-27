import { colors } from "@/styles/colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    container: {
        width: "100%",
        // height: 50,
        // backgroundColor: colors.gray[800],
        borderRadius: 15,
        paddingHorizontal: 16,
        flexDirection: "row",
        justifyContent: "space-between",
        alignItems: "center",
        borderBottomWidth: 1,
        borderColor: colors.gray[500]
    },
    input: {
        fontSize: 15,
        fontFamily: "Poppins-Regular",
        width: "90%",
        color: colors.gray[100]
    }
})