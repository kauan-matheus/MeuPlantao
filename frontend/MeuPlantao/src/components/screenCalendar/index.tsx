import { View, KeyboardAvoidingView, Platform, ActivityIndicator } from "react-native";
import { LocaleConfig, Calendar} from 'react-native-calendars';
import { useCallback, useEffect, useState } from "react";
import { useFocusEffect } from "expo-router";
import dayjs from "dayjs"

import { styles } from "./styles";
import { colors } from "@/styles/colors";
import { Plantao } from "@/utils/objects";

import { ListPlantao } from "../listPlantao";
import { Input } from "../input/input";
import { getPlantoes } from "@/services/plantao";

LocaleConfig.locales['pt-br'] = {
    monthNames: [
        'Janeiro','Fevereiro','Março','Abril','Maio','Junho',
        'Julho','Agosto','Setembro','Outubro','Novembro','Dezembro'
    ],
    monthNamesShort: [
        'Jan','Fev','Mar','Abr','Mai','Jun',
        'Jul','Ago','Set','Out','Nov','Dez'
    ],
    dayNames: [
        'Domingo','Segunda','Terça','Quarta','Quinta','Sexta','Sábado'
    ],
    dayNamesShort: [
        'Dom','Seg','Ter','Qua','Qui','Sex','Sáb'
    ],
    today: 'Hoje'
}

LocaleConfig.defaultLocale = 'pt-br'

export function ScreenCalendar() {

    useEffect(() => {
        async function load() {
            setLoading(true)
            const data = await getPlantoes()
            if (data.type === "success"){
                setPlantoes(data.result)
            }
            else
                console.log(data.result)
            setLoading(false)
        }

        load()
    }, [])

    const [daySelected, setDaySelected] = useState(dayjs().format("YYYY-MM-DD"))
    const [search, setSearch] = useState("")
    const [plantoes, setPlantoes] = useState<Plantao[]>([])
    const [plantao, setPlantao] = useState<Plantao[]>([])
    const [loading, setLoading] = useState(false)

    function getPlantao() {
        const filtered = plantoes.filter(p => p.date === dayjs(daySelected).format("DD/MM/YYYY") && (p.locale.toUpperCase().includes(search.toUpperCase()) || p.sector.toUpperCase().includes(search.toUpperCase())))

        setPlantao(filtered)
    }

    useFocusEffect(
        useCallback(() => {
            getPlantao()
        }, [daySelected, search, plantoes])
    )

    return (
        <KeyboardAvoidingView behavior={Platform.OS === "ios" ? "padding" : "height"} style={styles.container}>
            <Calendar
            style={styles.calendar}
            onDayPress={(day) => {
                setDaySelected(day.dateString)
            }}
            markedDates={{
                [daySelected]: {
                    selected: true,
                    selectedColor: colors.blue[500],
                },
            }}
            theme={{
                calendarBackground: colors.gray[600],
                textSectionTitleColor: colors.gray[500],
                selectedDayTextColor: colors.gray[700],
                todayTextColor: colors.blue[500],
                dayTextColor: colors.gray[300],
                textDisabledColor: colors.gray[500],
                monthTextColor: colors.blue[400],
                arrowColor: colors.gray[300],
                textDayFontFamily: "Poppins-Regular",
                textMonthFontFamily: "Poppins-Bold",
                textDayHeaderFontFamily: "Poppins-Regular"
            }}
            />

            <View style={styles.list}>
                <Input 
                type="text"
                icon="search-sharp"
                placeholder="Pesquisar"
                onChangeText={setSearch}
                />
                {
                    loading
                    ? <ActivityIndicator style={[{marginTop: 20}]} size="large" color={colors.blue[400]} />
                    :<ListPlantao plantao={plantao} showFooter={true} isEmpty="Não há plantões para solicitar" />
                }
            </View>
        </KeyboardAvoidingView>
    )
}