import { View, Text, FlatList } from "react-native";
import { LocaleConfig, Calendar} from 'react-native-calendars';
import { useCallback, useState } from "react";

import { styles } from "./styles";
import { colors } from "@/styles/colors";
import { plantoes, Plantao } from "@/utils/plantoes";

import { PlantaoItem } from "../plantaoItem";
import { useFocusEffect } from "expo-router";

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

    const [daySelected, setDaySelected] = useState(new Date().toISOString().substring(0, 10))
    const [plantao, setPlantao] = useState<Plantao[]>([])

    function getPlantao() {
        const filtered = plantoes.filter(p => p.date === new Date(daySelected).toLocaleDateString("pt-br"))

        setPlantao(filtered)
    }

    useFocusEffect(
        useCallback(() => {
            getPlantao()
        }, [daySelected])
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
                arrowColor: colors.gray[300],
                monthTextColor: colors.blue[400],
                textMonthFontWeight: 'bold',
            }}
            />

            <View style={styles.list}>
                <FlatList
                data={plantao}
                keyExtractor={(item) => item.id}
                renderItem={({ item }) => (
                    <PlantaoItem 
                    date={item.date}
                    start={item.start}
                    duration={item.duration}
                    locale={item.locale}
                    sector={item.sector}
                    oncall={item.oncall}
                    />
                )}
                style={styles.listContent}
                ListEmptyComponent={() => (
                    <Text style={styles.textListEmpty}>Nenhum plantão disponível</Text>
                )}
                ListFooterComponent={() => (
                    <View style={styles.footer}></View>
                )}
                showsVerticalScrollIndicator={false}
                />
            </View>
        </View>
    )
}