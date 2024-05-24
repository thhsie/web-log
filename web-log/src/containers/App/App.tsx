import "@mantine/core/styles.css";
import { MantineProvider } from "@mantine/core";
import { theme } from "../../theme";
import { Route, Routes } from "react-router-dom";
import { routes } from "../../routes/routes";
import { LandingPage } from "../LandingPage/LandingPage";
import { Header } from "../../components/Header/Header";
import { Sidebar } from "../../components/Sidebar/Sidebar";
import { Page } from "../Page/Page";
import { CreateBlog } from "../../components/CreateBlog/CreateBlog";
import { BlogList } from "../../components/BlogList/BlogList";

export const App: React.FC = () => {
  return (
    <MantineProvider theme={theme}>
      <Header />
      {/* Main app node */}
      <Routes>
        <Route path={routes.LANDING} element={<LandingPage />} />
        <Route
          path={routes.CREATE_BLOG}
          element={
            <Page>
              <CreateBlog />
            </Page>
          }
        />
        <Route
          path={routes.BLOGS_VIEW}
          element={
            <Page>
              <BlogList />
            </Page>
          }
        />
      </Routes>
    </MantineProvider>
  );
};
