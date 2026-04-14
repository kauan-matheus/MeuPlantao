import { Ionicons } from "@expo/vector-icons"

type Option = {
    id: string
    name: string
    icon: keyof typeof Ionicons.glyphMap
}

export const options: Option[] = [
    { id: "1", name: "Home", icon: "home-outline" },
    { id: "2", name: "Calendar", icon: "calendar-outline" },
    { id: "3", name: "History", icon: "list-outline" },
    { id: "4", name: "Profile", icon: "person-outline" }
]