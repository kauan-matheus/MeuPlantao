import { FlatList, View, Text, Image } from "react-native";
import { useState } from "react";
import { useFonts } from "expo-font";

import { styles } from "./styles";
import { options } from "@/utils/options";

import { NavLink } from "@/components/navLink";
import { ScreenHome } from "@/components/screenHome";
import { ScreenCalendar } from "@/components/screenCalendar";
import { ScreenHistory } from "@/components/screenHistory";
import { ScreenProfile } from "@/components/screenProfile";

export default function InterfaceUser() {

    const [option, setOption] = useState(options[0].name)
    
    const [fonts] = useFonts({
        'Poppins-Regular': require('@/assets/fonts/Poppins-Regular.ttf'),
        'Poppins-Bold': require('@/assets/fonts/Poppins-Bold.ttf'),
    })

    if (!fonts) {
        return null;
    }

    return (
        <View style={styles.container}>
            {option !== "Profile" && (
                <View style={styles.top}>
                    <View style={styles.topDiv}>
                        <Image source={require("@/assets/images/profile.jpg")} style={styles.imageProfile} />
                    </View>
                    <View style={styles.topDiv}>
                        <Text style={styles.topText}>Olá, User da Silva 👋</Text>
                    </View>
                </View>
            )}
            <View style={styles.content}>
                {option === "Home" ? <ScreenHome /> :
                option === "Calendar" ? <ScreenCalendar /> :
                option === "History" ? <ScreenHistory /> :
                option === "Profile" ? <ScreenProfile /> : 
                null}
            </View>
            <FlatList
            data={options}
            keyExtractor={(item) => item.id}
            renderItem={({ item }) => (
                <NavLink
                icon={item.icon}
                isSelected={item.name === option}
                onPress={() => setOption(item.name)}
                />
            )}
            horizontal
            style={styles.navBar}
            contentContainerStyle={styles.navBarContent}
            />
        </View>
    )
}