import { CapacitorConfig } from '@capacitor/cli';

const config: CapacitorConfig = {
  appId: 'com.example.app',
  appName: 'wBialy',
  webDir: 'dist/web',
  server: {
    androidScheme: 'https'
  }
};

export default config;
