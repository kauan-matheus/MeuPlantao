import { View, Text, FlatList } from "react-native";
import { LocaleConfig, Calendar} from 'react-native-calendars';
import { useCallback, useState } from "react";
import { useFocusEffect } from "expo-router";
import dayjs from "dayjs"

import { styles } from "./styles";
import { colors } from "@/styles/colors";
import { plantoes, Plantao } from "@/utils/plantoes";

import { ListPlantao } from "../listPlantao";
import { Input } from "../input/input";

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

    const [daySelected, setDaySelected] = useState(dayjs().format("YYYY-MM-DD"))
    const [search, setSearch] = useState("")
    const [plantao, setPlantao] = useState<Plantao[]>([])

    function getPlantao() {
        const filtered = plantoes.filter(p => p.date === dayjs(daySelected).format("DD/MM/YYYY") && (p.locale.toUpperCase().includes(search.toUpperCase()) || p.sector.toUpperCase().includes(search.toUpperCase())))

        setPlantao(filtered)
    }

    useFocusEffect(
        useCallback(() => {
            getPlantao()
        }, [daySelected, search])
    )

    return (
        <View style={styles.container}>
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
                placeholder="Procure pelo local..."
                onChangeText={setSearch}
                />
                <ListPlantao plantao={plantao} />
            </View>
        </View>
    )
}