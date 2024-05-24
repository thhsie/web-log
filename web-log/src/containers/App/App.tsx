import "@mantine/core/styles.css";
import { Scrollbar } from "../../components/Scrollbar/Scrollbar";
import { MantineProvider } from "@mantine/core";
import { theme } from "../../theme";
import { Route, Routes } from "react-router-dom";
import { routes } from "../../routes/routes";
import { LandingPage } from "../LandingPage/LandingPage";
import { Header } from "../../components/Header/Header";

export const App: React.FC = () => {
  return (
    <MantineProvider theme={theme}>
      <Header />
      <Scrollbar />
      {/* Main app node */}
      <Routes>
        <Route path={routes.LANDING} element={<LandingPage />} />
      </Routes>
    </MantineProvider>
  );
};
