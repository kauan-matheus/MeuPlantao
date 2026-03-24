import { Ionicons } from "@expo/vector-icons"

type Option = {
    id: string
    name: string
    icon: keyof typeof Ionicons.glyphMap
}

export const options: Option[] = [
    { id: "1", name: "Home", icon: "globe-outline" },
    { id: "2", name: "Plantões", icon: "book-outline" },
    { id: "3", name: "Perfil", icon: "person-circle-outline" }
]