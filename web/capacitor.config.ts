import { CapacitorConfig } from '@capacitor/cli';

const config: CapacitorConfig = {
  appId: 'com.example.app',
  appName: 'wbialyApka',
  webDir: 'dist/web',
  server: {
    androidScheme: 'https'
  }
};

export default config;
