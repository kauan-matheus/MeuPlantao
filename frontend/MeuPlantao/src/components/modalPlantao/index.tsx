import { View } from "react-native"
import MapView, { Marker, PROVIDER_GOOGLE } from "react-native-maps";

import { styles } from "./styles";

export function ModalPlantao() {
    const map = {
        locale: "Unimar",
        latitude: -22.23660479234184,
        longitude: -49.966478761047746
    }  
    
    return (
        <View style={styles.container}>
            <MapView
                provider={PROVIDER_GOOGLE}
                style={[{flex: 1}]}
                initialRegion={{
                latitude: map.latitude,
                longitude: map.longitude,
                latitudeDelta: 0.01,
                longitudeDelta: 0.01,
                }}
            >
                <Marker
                    coordinate={{
                        latitude: map.latitude,
                        longitude: map.longitude,
                    }}
                    title={map.locale}
                    />
            </MapView>
        </View>
    )
}