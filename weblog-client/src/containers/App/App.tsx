import "@mantine/core/styles.css";
import '@mantine/notifications/styles.css';
import { MantineProvider } from "@mantine/core";
import { theme } from "../../theme";
import { Route, Routes } from "react-router-dom";
import { routes } from "../../routes/routes";
import { LandingPage } from "../LandingPage/LandingPage";
import { Header } from "../../components/Header/Header";
import { Page } from "../Page/Page";
import { CreateBlog } from "../../components/CreateBlog/CreateBlog";
import { BlogList } from "../../components/BlogList/BlogList";
import { BlogPost } from "../../components/BlogPost/BlogPost";
import { Notifications } from "@mantine/notifications";
import { EditBlog } from "../../components/EditBlog/EditBlog";
import { BaseUrlProvider } from "../../contexts/BaseUrlContext";

export const App: React.FC = () => {
  return (
    <MantineProvider theme={theme}>
      <Notifications />
      <BaseUrlProvider>
        <Header />
        {/* Main app node */}
        <Routes>
          <Route path={"*"} element={<LandingPage />} />
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
          <Route
            path={routes.BLOG}
            element={
              <Page>
                <BlogPost />
              </Page>
            }
          />
          <Route
            path={routes.BLOG_EDIT}
            element={
              <Page>
                <EditBlog />
              </Page>
            }
          />
        </Routes>
      </BaseUrlProvider>
    </MantineProvider>
  );
};
