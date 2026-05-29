import { View, Text, Image, ActivityIndicator, Pressable } from "react-native";
import { useEffect, useState } from "react";
import * as ImagePicker from 'expo-image-picker';

import { styles } from "./styles";
import { Button } from "../button";
import { colors } from "@/styles/colors";
import { router } from "expo-router";
import { About } from "../about";

import { getAuth, logout, uploadFotoPerfil } from "@/services/user";
import { getProfessional } from "@/services/professional";

import { Professional } from "@/utils/objects";
import { Ionicons } from "@expo/vector-icons";

export function ScreenProfile() {
    
    useEffect(() => {
        async function load() {
            setLoading(true)
            const auth = await getAuth()
            const data = await getProfessional(auth.user.id)
            setData(data)
            setFotoPerfilUrl(auth.user.fotoPerfilUrl)
            setLoading(false)
        }

        load()
    }, [])

    const handleEditarFotoPerfil = async () =>{
        try{
            const result = await ImagePicker.launchImageLibraryAsync({
                mediaTypes: ['images'],
                quality: 1,
            });

            if (result.canceled) {
                return;
            }

            const response = await uploadFotoPerfil(result.assets[0]);

            if (response?.type === "success") {
                setFotoPerfilUrl(`${response.message}?t=${Date.now()}`);
            }
        }catch(error: any){
            return {type: "error", message: error.response?.data}
        }
    };

    const [data, setData] = useState<Professional>()
    const [fotoPerfilUrl, setFotoPerfilUrl] = useState("")
    
    const [loading, setLoading] = useState(false)

    return (
        <View style={styles.container}>
            <Pressable style={styles.imageContainer} onPress={handleEditarFotoPerfil}>
                <Image
                    source={
                        typeof fotoPerfilUrl === "string" &&
                        fotoPerfilUrl.trim().length > 0
                        ? { uri: `${fotoPerfilUrl}?t=${Date.now()}` }
                        : require("../../assets/images/profile.jpg")
                    }
                    style={styles.imageProfile}
                />
                <View style={styles.editBadge}>
                    <Ionicons
                        name="camera"
                        size={18}
                        color="white"
                    />
                </View>
            </Pressable>
            <Text style={styles.name}>
                {data?.nome}
            </Text>

            <Text style={styles.role}>
                {data?.crm ? "Médico" : "Enfermeiro"}
            </Text>

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