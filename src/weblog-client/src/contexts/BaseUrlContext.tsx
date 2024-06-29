import { createContext, useState, ReactNode, useContext } from 'react';
import { apiUrl } from '../api/WeblogClient';

interface BaseUrlContextValue {
  baseUrl: string;
  setBaseUrl: (url: string) => void;
}

const BaseUrlContext = createContext<BaseUrlContextValue | undefined>(undefined);

export const BaseUrlProvider = ({ children }: { children: ReactNode }) => {
  const [baseUrl, setBaseUrl] = useState<string>(apiUrl.DEV);

  return (
    <BaseUrlContext.Provider value={{ baseUrl, setBaseUrl }}>
      {children}
    </BaseUrlContext.Provider>
  );
};

export const useBaseUrl = () => {
  const context = useContext(BaseUrlContext);
  if (context === undefined) {
    throw new Error('useBaseUrl must be used within a BaseUrlProvider');
  }
  return context;
};
