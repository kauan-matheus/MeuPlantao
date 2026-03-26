import { View, Text } from "react-native";
import { LocaleConfig, Calendar} from 'react-native-calendars';
import { useState } from "react";

import { styles } from "./styles";
import { colors } from "@/styles/colors";

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

    const [daySelected, setDaySelected] = useState(new Date().toISOString().substring(0, 10));

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
                monthTextColor: colors.gray[200],
                textMonthFontWeight: 'bold',
            }}
            />

            <View style={styles.list}>
                <Text style={styles.listTitle}>Plantões disponíveis</Text>
            </View>
        </View>
    )
}