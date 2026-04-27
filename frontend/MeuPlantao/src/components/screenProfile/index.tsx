import { View, Text, Image, ActivityIndicator } from "react-native";
import { useEffect, useState } from "react";

import { styles } from "./styles";
import { Button } from "../button";
import { colors } from "@/styles/colors";
import { router } from "expo-router";
import { About } from "../about";

import { getAuth, logout } from "@/services/user";
import { getProfessional } from "@/services/professional";

import { Professional } from "@/utils/objects";

export function ScreenProfile() {
    
    useEffect(() => {
        async function load() {
            setLoading(true)
            const auth = await getAuth()
            const data = await getProfessional(auth.user.id)
            setData(data)
            setLoading(false)
        }

        load()
    }, [])

    const [data, setData] = useState<Professional>()
    
    const [loading, setLoading] = useState(false)

    return (
        <View style={styles.container}>
            <Image source={require("@/assets/images/profile.jpg")} style={styles.imageProfile} />
            {!loading ? (
                <View style={styles.dataUser}>
                    <About title="Nome" text={data?.nome} />
                    <About title={data?.crm ? "CRM": "Coren"} text={data?.crm ? data?.crm : data?.coren} />
                    <About title="Telefone" text={data?.telefone} />
                </View>
            ) : (
                <ActivityIndicator size="large" color={colors.blue[400]} />
            )}
            <Button
            text="Sair da conta"
            color={colors.red[200]}
            textColor={colors.gray[600]}
            onPress={async () => {
                if (loading) return
                
                setLoading(true)
                logout()
                setLoading(false)

                router.back()
            }}
            />
        </View>
    )
}